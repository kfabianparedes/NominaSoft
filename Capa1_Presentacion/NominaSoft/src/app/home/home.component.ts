import { Component, OnInit } from '@angular/core';
import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import { NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormBuilder,  Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RecursosModule } from '../recursos/recursos.module';
import { EmpleadoService } from '../services/empleado.service';
import { Empleado } from '../models/empleado.model';
import { PeriodoDePago } from '../models/periodo.model';
import { PeriodoPagoService} from '../services/periodo-pago.service';
import { PagoService } from '../services/pago.service';
import { ContratoService } from '../services/contrato.service';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  //cargando: boolean = false;
  mostrarAlerta = false;
  mensaje :any;
  tipoAlerta: string = '';
  closeResult = '';
  usuarioForm: any;
  mostrar:boolean = false;
  empleado!: Empleado;
  periodo!: PeriodoDePago;
  listaContratos: any[] = [];   
  constructor(
    private modalService: NgbModal,
    private formBuilder: FormBuilder,
    private router: Router,
    private empleadoService: EmpleadoService,
    private periodoPagoService: PeriodoPagoService,
    private contratoService: ContratoService,
    private pagoService : PagoService,
    ) { }
   
  ngOnInit(): void {
    //this.cargando = true;
    this.inicializarUsuarioForm();
  }
  inicializarUsuarioForm(){
    this.usuarioForm = this.formBuilder.group({
      dni: ['' , [Validators.required , Validators.minLength(8),Validators.maxLength(8),Validators.pattern(/^([0-9])*$/)]],
    });
  }
  open(gestionar_contrato: any) {
    this.modalService.open(gestionar_contrato, {ariaLabelledBy: 'modal-basic-title'}).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }
  get dni() {
    return this.usuarioForm.get('dni');
  }
  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      this.resetformulario(); //para reiniciar el formulario cada vez que cierro el modal con ESC
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      this.resetformulario(); //para reiniciar el formulario cada vez que cierro el modal con dando click fuera
      return 'by clicking on a backdrop';
    } else {
      
      return `with: ${reason}`;
    }
  }
  resetformulario(){
    this.usuarioForm.reset();
  }
  obtenerEmpleadoPorDNI(){
    this.empleadoService.obtenerEmpleadoPorDNI(this.dni.value).subscribe(
      data =>{
        
        this.tipoAlerta = 'success';
        this.mostrarAlerta = true;
        this.mensaje = 'Solicitud ejecutada con éxito';
        this.empleado = data;
        setTimeout(() =>{
          this.router.navigate(['contrato',this.empleado.idEmpleado]),
          this.modalService.dismissAll();
        },1000);
        
      },
      error =>{
        console.log(error);
        if(error['error']['mensaje']==='No existe el empleado'){
          this.tipoAlerta = 'danger';
          this.mostrarAlerta = true;
          this.mensaje = error['error']['mensaje'];
        }
        /*if (error['mensaje'] !== undefined) {
            this.mensaje = error['mensaje'];//' Hubo un error al intentar buscar al empleado. Por favor, intente otra vez.';
        }else {
          this.mensaje = 'No se pudo obtener los datos, debido a la falla de alguna conexión. Por favor, intente otra vez.';
        }*/
      }
  )}

  pagoActivo(){
    this.periodoPagoService.obtenerPeriodoPagoActivo().subscribe(
      data =>{
        this.periodo = data;
        this.mostrar = true;
      },
      error =>{
        this.tipoAlerta = 'danger';
        this.mostrarAlerta = true;
        this.mensaje =error['error']['mensaje'];
 
      }
  )}
  procesarPago(){
    this.periodoPagoService.verificarProcesarPeriodo(this.periodo).subscribe(
      data =>{
        if(data==true){
          this.tipoAlerta = 'success';
          this.mostrarAlerta = true;
          this.mensaje = 'Solicitud ejecutada con éxito';
          //PROCESAR PAGO
          this.contratoService.listarContratosPorIdPeriodo(this.periodo.idPeriodo).subscribe(data => {
            this.listaContratos = data; 
            if(this.listaContratos.length===0) {
                this.tipoAlerta = 'danger';
                this.mostrarAlerta = true;
                this.mensaje = 'No se puede procesar porque no existen contratos.';
              }
            else{
              //Aqui va el proceso del pago
              this.cambiarProcesado(this.periodo.idPeriodo);
            }    
            },
            error => {
              this.tipoAlerta = 'danger';
              this.mostrarAlerta = true;
              if (error['error']['mensaje'] !== undefined) {
                if (error['error']['mensaje'] ==="No se puede procesar porque no existe un periodo de pago activo.") 
                {
                  this.mensaje = error['error']['mensaje'];
                }
              }else {
                this.mensaje = 'No se pudo obtener los datos, debido a la falla de alguna conexión. Por favor, intente otra vez.';
              }
            });
        }
        else{
          this.tipoAlerta = 'danger';
          this.mostrarAlerta = true;
          this.mensaje = 'No se puede procesar el periodo porque la fecha actual debe ser mayor o igual a la fecha fin del periodo de pago';
        }
      },
      error =>{
        this.tipoAlerta = 'danger';
        this.mostrarAlerta = true;
        if (error['error']['mensaje'] ==="No se puede procesar porque no existe un periodo de pago activo.") {
          this.mensaje = error['error']['mensaje'];}

        if (error['error']['mensaje'] !== undefined) {
            this.mensaje = ' Hubo un error al intentar buscar al periodo de pago activo. Por favor, intente otra vez.';
        }else {
          this.mensaje = 'No se pudo obtener los datos, debido a la falla de alguna conexión. Por favor, intente otra vez.';
        }
      }
  )}
  cambiarProcesado(idPeriodo:number){
    this.periodoPagoService.cambiarProcesado(idPeriodo).subscribe(
      data =>{
        this.realizarPago();
      }
      ,error =>{
        this.tipoAlerta = 'danger';
        this.mostrarAlerta = true;
        if (error['error']['mensaje'] !== undefined) {
          if (error['error']['mensaje'] ==="No se pudo procesar el periodo de pago.") 
          {
            this.mensaje = error['error']['mensaje'];
          }
        }else {
          this.mensaje = 'No se pudo obtener los datos, debido a la falla de alguna conexión. Por favor, intente otra vez.';
        }
      });
  }
  
  realizarPago(){
    this.pagoService.realizarPagoContratos(this.listaContratos).subscribe(
      data=>{
        console.log(data);
        setTimeout(() =>{
          this.router.navigate(['pago/pago-realizado',this.periodo.idPeriodo]),
          this.modalService.dismissAll();
        },1000);
      },error =>{
        this.tipoAlerta = 'danger';
        this.mostrarAlerta = true;
        if (error['error']['mensaje'] !== undefined) {
          if (error['error']['mensaje'] ==="Hubo un error al realizar el pago de los contratos. Vuelva a intentarlo.") 
          {
            this.mensaje = error['error']['mensaje'];
          }
        }else {
          this.mensaje = 'No se pudo obtener los datos, debido a la falla de alguna conexión. Por favor, intente otra vez.';
        }
      });
    }
}
