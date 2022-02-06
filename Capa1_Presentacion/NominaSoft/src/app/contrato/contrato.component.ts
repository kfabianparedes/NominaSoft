import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormBuilder,  Validators } from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import { Empleado } from '../models/empleado.model';
import { EmpleadoService } from '../services/empleado.service';
import {ContratoService} from '../services/contrato.service';
import { Contrato } from '../models/contrato.model';
import { SeguroService } from '../services/seguro.service';
import { Seguro } from '../models/seguro.model';
import { DatePipe } from '@angular/common';


@Component({
  selector: 'app-contrato',
  templateUrl:'./contrato.component.html',
  styleUrls: ['./contrato.component.css']
})
export class ContratoComponent implements OnInit {

 
  mostrarAlerta = false;
  mostrarAlerta_crear = false;
  mostrarAlerta_editar = false;
  mostrarAlerta_anular = false;
  mensaje :any;
  tipoAlerta: string = '';
  closeResult = '';
  mostrar:boolean = false; //para mostrar el html
  mostrar_editar_modal:boolean = false;
  mostrar_anular_modal:boolean = false;
  asignacion:boolean = false;
  nueva_asignacion:boolean = false;
  tiene_asignacion:  boolean = false;
  no_tiene_asignacion:boolean = false;
  contrato_ : any;
  fecha: any;
  listaSeguros: Seguro[] = [];
  empleado!: Empleado;
  contrato!:Contrato;
  afp: any;
  contrato_anular:any;
  nuevo_contrato:any;
  editar_contrato:any;
  editarContratoForm:any;
  crearContratoForm: any;
  constructor(private modalService: NgbModal,
              private router: Router,
              private activatedRoute :ActivatedRoute,
              private empleadoService:EmpleadoService,
              private contratoService:ContratoService,
              private seguroService:SeguroService,
              private formBuilder: FormBuilder,
              private datePipe: DatePipe
              ) { }

  ngOnInit(): void {
    this.obtenerSeguros();
    this.activatedRoute.params.subscribe( data => { //recibo el idEmpleado por la ruta y lo almaceno en data
      this.obtenerEmpleadoPorId(data['id']);
    });
    this.inicializarCrearContratoForm();
    
  }
  @ViewChild('editar') modalEditar!: ElementRef;
  @ViewChild('anular') modalAnular!: ElementRef;
  @ViewChild('crear') modalCrear!: ElementRef;
  @ViewChild('confimarAnulacion') modalConfirmarAnulacion!: ElementRef;

  inicializarCrearContratoForm(){
    this.crearContratoForm = this.formBuilder.group({
      crear_cargo: ['', [Validators.required]],
      crear_seguro: ['',[Validators.required]],
      crear_fechaInicial:['',Validators.required],
      crear_fechaFin:['',Validators.required],
      crear_totalHorasPorSemana:['',Validators.required],
      crear_valorPorHora:['',Validators.required],
    });
  }
  inicializarEditarContratoForm(){
    this.editarContratoForm = this.formBuilder.group({
      cargo: ['', [Validators.required]],
      seguro: ['',[Validators.required]],
      fechaInicial:['',Validators.required],
      fechaFin:['',Validators.required],
      totalHorasPorSemana:['',[Validators.required,Validators.pattern(/[0-9]/)]],
      valorPorHora:['',[Validators.required,Validators.pattern(/[0-9]/)]],
    });
    this.fechaInicial.setValue(this.datePipe.transform(this.contrato_.fechaInicial, 'yyyy-MM-dd'));
    this.fechaFin.setValue(this.datePipe.transform(this.contrato_.fechaFinal, 'yyyy-MM-dd'));
    this.totalHorasPorSemana.setValue(this.contrato_.totalHorasPorSemana);
    this.valorPorHora.setValue(this.contrato_.valorPorHora);
    this.cargo.setValue(this.contrato_.cargo);
  }
  get cargo(){
    return this.editarContratoForm.get('cargo');
  }
  get seguro() {
    return this.editarContratoForm.get('seguro');
  }
  get fechaInicial(){
    return this.editarContratoForm.get('fechaInicial');
  }
  get fechaFin(){
    return this.editarContratoForm.get('fechaFin');
  }
  get totalHorasPorSemana(){
    return this.editarContratoForm.get('totalHorasPorSemana');
  }
  get valorPorHora(){
    return this.editarContratoForm.get('valorPorHora');
  }
  get asignacion_familiar(){
    return this.editarContratoForm.get('asignacion_familiar');
  }
  // datos del formulario de crear contrato_
  get crear_cargo(){
    return this.crearContratoForm.get('crear_cargo');
  }
  get crear_seguro() {
    return this.crearContratoForm.get('crear_seguro');
  }
  get crear_fechaInicial(){
    return this.crearContratoForm.get('crear_fechaInicial');
  }
  get crear_fechaFin(){
    return this.crearContratoForm.get('crear_fechaFin');
  }
  get crear_totalHorasPorSemana(){
    return this.crearContratoForm.get('crear_totalHorasPorSemana');
  }
  get crear_valorPorHora(){
    return this.crearContratoForm.get('crear_valorPorHora');
  }
  get crear_asignacion_familiar(){
    return this.crearContratoForm.get('crear_asignacion_familiar');
  }
  open(content: any) {
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'}).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      this.resetearFormularios();
      this.resetModal_Editar();
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      this.resetearFormularios();
      this.resetModal_Editar();
      return 'by clicking on a backdrop';
    } else {
      this.resetearFormularios();
      this.resetModal_Editar();
      return `with: ${reason}`;
    }
  }
  resetearFormularios(){
    this.mostrarAlerta = false;
    this.mostrarAlerta_crear = false;
    this.mostrarAlerta_editar = false;
    this.editarContratoForm.reset();
    this.crearContratoForm.reset();
  }
  obtenerSeguros(){
    this.seguroService.listarSeguros().subscribe(
      data => {
        this.listaSeguros = data;
        //console.log(this.listaSeguros);
      },
      error =>{
        this.tipoAlerta = 'danger';
        this.mostrarAlerta = true;
        if (error['error']['error'] !== undefined) {
            this.mensaje = ' Hubo un error al intentar listar los seguros. Por favor, intente otra vez.';
        }else {
          this.mensaje = 'No se pudo obtener los seguros, debido a la falla de alguna conexión. Por favor, intente otra vez.';
        }
      }
    );
  }
  obtenerEmpleadoPorId(idEmpleado:number){
    this.empleadoService.obtenerEmpleadoPorId(idEmpleado).subscribe(
      data =>{
        //this.empleado = data;
        this.empleado = data;
        this.mostrar = true;
      },
      error =>{
        this.tipoAlerta = 'danger';
        this.mostrarAlerta = true;
        if (error['error']['error'] !== undefined) {
            this.mensaje = ' Hubo un error al intentar buscar al empleado. Por favor, intente otra vez.';
        }else {
          this.mensaje = 'No se pudo obtener los datos, debido a la falla de alguna conexión. Por favor, intente otra vez.';
        }
      }
  )}
  obtenerContratoPorIdEmpleado(){
    
    this.contratoService.obtenerContratoPorIdEmpleado(this.empleado.idEmpleado).subscribe(
      data=>{
        
        this.contrato_ = data;
        this.contrato_anular = data;
        if(this.contrato_.tieneAsignacionFamiliar === true){
          this.tiene_asignacion = true;
        }else{
          this.no_tiene_asignacion = true;
        }
        this.seguroService.obtenerSeguroPorId(this.contrato_.seguro.idSeguro).subscribe(
          data=>{
            this.afp = data['afp'];
          },
          error=>{
            this.tipoAlerta = 'danger';
            this.mostrarAlerta_editar = true;
            if (error['error']['mensaje'] !== undefined) {
                this.mensaje = error['error']['mensaje'];
            }else {
              this.mensaje = 'No se pudo obtener el campo AFP, debido a la falla de alguna conexión. Por favor, intente otra vez.';
            }
          }  
        );
        this.inicializarEditarContratoForm();
        this.obtenerSeguros();
        
        this.mostrar_editar_modal=true;
      },error=>{
        this.tipoAlerta = 'danger';
        this.mostrarAlerta_editar = true;
        if (error['error']['mensaje'] == 'No existe un contrato vigente') {
          this.mensaje = error['error']['mensaje'];
        }else {
          this.mensaje = 'No se pudo obtener los datos, debido a la falla de alguna conexión. Por favor, intente otra vez.';
        }
      }
    )
    this.open(this.modalEditar);
  }
  obtenerContratoPorIdEmpleado_anular(){
    
    this.contratoService.obtenerContratoPorIdEmpleado(this.empleado.idEmpleado).subscribe(
      data=>{
        
        this.contrato_ = data;
        this.contrato_anular = data;
        if(this.contrato_.tieneAsignacionFamiliar === true){
          this.tiene_asignacion = true;
        }else{
          this.no_tiene_asignacion = true;
        }
        this.seguroService.obtenerSeguroPorId(this.contrato_.seguro.idSeguro).subscribe(
          data=>{
            this.afp = data['afp'];
          },
          error=>{
            this.tipoAlerta = 'danger';
            this.mostrarAlerta_anular = true;
            if (error['error']['error'] !== undefined) {
                this.mensaje = ' Hubo un error al intentar mostrar el AFP.';
            }else {
              this.mensaje = 'No se pudo obtener el campo AFP, debido a la falla de alguna conexión. Por favor, intente otra vez.';
            }
          }  
        );
        this.inicializarEditarContratoForm();
        this.obtenerSeguros();
        this.mostrar_anular_modal=true;
        
      },error=>{
        this.tipoAlerta = 'danger';
        this.mostrarAlerta_anular = true;
        if (error['error']['mensaje'] == 'No existe un contrato vigente') {
          this.mensaje = error['error']['mensaje'];
        }else {
          this.mensaje = 'No se pudo obtener los datos, debido a la falla de alguna conexión. Por favor, intente otra vez.';
        }
      }
    )
    
  }

  obtenerAsignacionFamiliar(asignacion: boolean){
    this.asignacion = asignacion;
    if(this.asignacion === true){
      this.tiene_asignacion = true;
      this.no_tiene_asignacion = false;
    }else{
      this.tiene_asignacion = false;
      this.no_tiene_asignacion = true;
    }
  }
  crearAsignacionParaContrato(asignacion: boolean){
    this.nueva_asignacion = asignacion;
  }
  resetModal_Editar(){
    this.tiene_asignacion = false;
    this.no_tiene_asignacion = false;
  }
  editarContrato(){
    
    this.editar_contrato={
      "idContrato": this.contrato_.idContrato,
      "EsVigente": true,
      "EstaAnulado": false,
      "FechaInicial": this.fechaInicial.value,
      "FechaFinal": this.fechaFin.value,
      "TieneAsignacionFamiliar": this.asignacion,
      "TotalHorasPorSemana": this.totalHorasPorSemana.value,
      "ValorPorHora": this.valorPorHora.value,
      "Cargo": this.cargo.value,
      "Empleado": {"IdEmpleado":"10"},
      "Seguro": {"IdSeguro": this.seguro.value},
    }
    console.log(this.editar_contrato);
    this.contratoService.editarContrato(this.editar_contrato).subscribe(
      data =>{
        if(data!=null){
          this.tipoAlerta = 'success';
          this.mostrarAlerta_editar = true;
          if (data== 0) {
            this.tipoAlerta = 'danger';
            this.mostrarAlerta_editar = true;
              this.mensaje = 'No se pudo editar el contrato. Por favor, intente otra vez.';
          }else if(data === 1){
            this.mensaje = 'Se editó el contrato correctamente.';
          }
        }
      },
      error =>{
        console.log(error);
        this.tipoAlerta = 'danger';
        this.mostrarAlerta_editar = true;
        if (error['error']['mensaje'] == 'No se guardó las modificaciones del contrato”. El contrato debe durar como mínimo 3 meses.') {
          this.mensaje = error['error']['mensaje'];  
        }
        if(error['error']['mensaje'] =='No se guardó las modificaciones del contrato”. El total de horas por semana debe ser múltiplo de 4 que estan entre 8 y 40' ){
          this.mensaje = error['error']['mensaje'];
        }
        if(error['error']['mensaje'] =='No se guardó las modificaciones del contrato”. El valor por hora no debe ser menor a 10 ni mayor a 60.' ){
          this.mensaje = error['error']['mensaje'];
        }
      }
    )
  }
  crearContrato(){
    this.nuevo_contrato={
      "EsVigente": true,
      "EstaAnulado": false,
      "FechaInicial": this.crear_fechaInicial.value,
      "FechaFinal": this.crear_fechaFin.value,
      "TieneAsignacionFamiliar": this.nueva_asignacion,
      "TotalHorasPorSemana": this.crear_totalHorasPorSemana.value,
      "ValorPorHora": this.crear_valorPorHora.value,
      "Cargo": this.crear_cargo.value,
      "Empleado": {"IdEmpleado":this.empleado.idEmpleado},
      "Seguro": {"IdSeguro":this.crear_seguro.value}
    }
    console.log(this.nuevo_contrato);
    this.contratoService.crearContrato(this.nuevo_contrato).subscribe(
      data =>{
        if(data!=null){
          this.tipoAlerta = 'success';
          this.mostrarAlerta_crear = true;
          if (data === 0) {
            this.tipoAlerta = 'danger';
            this.mostrarAlerta_crear = true;
              this.mensaje = 'No se pudo registrar el contrato. Por favor, intente otra vez.';
          }else if(data === 1){
            this.mensaje = 'Se guardo el nuevo contrato correctamente.';
          }
        }
      },
      error =>{
        this.tipoAlerta = 'danger';
        this.mostrarAlerta_crear = true;
        this.mensaje = error['error']['mensaje'];
      }
      )
  }
  anularContrato(){
    this.open(this.modalAnular);
    this.mostrarAlerta_anular = false ;
    this.obtenerContratoPorIdEmpleado_anular();
  }
  confimarAnulacionAbrir(){
    this.open(this.modalConfirmarAnulacion);
  }
  anularContratoAccion(){
    this.contratoService.anularContrato(this.contrato_anular.idContrato).subscribe(
      data=>{
        this.tipoAlerta = 'success';
        this.mostrarAlerta_anular = true;
        this.mensaje =" El contrato se ha anulado correctamente";
        console.log(data);
        setTimeout(() =>{
          this.modalService.dismissAll();
        },7000);
        
      },error =>{
        this.tipoAlerta = 'danger';
        this.mostrarAlerta_crear = true;
        if (error['error']['mensaje'] === 'Tiene que mandar la variable idContrato".') {
            this.mensaje = error['error']['mensaje'];
        }else if (error['error']['mensaje'] === 'El idContrato enviado no existe".' ){
          this.mensaje = 'No se pudieron enviar los datos, debido a la falla de alguna conexión. Por favor, intente otra vez.';
        }else {
          this.mensaje = 'No se pudieron enviar los datos, debido a la falla de alguna conexión. Por favor, intente otra vez.';
        }
      }
    );
  }
  crearNuevoContrato(){
    console.log(this.empleado.idEmpleado);
    this.contratoService.verificarExistenciaContratoVigente(this.empleado.idEmpleado).subscribe(
      data => {
        console.log(data);
        if(data===false){
          this.open(this.modalCrear);
        }
      },
      error => {
        this.tipoAlerta = 'danger';
        this.mostrarAlerta = true;
        this.mensaje = error['error']['mensaje'];
      }
  )}
}
