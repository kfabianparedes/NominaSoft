import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {PagoService} from '../../services/pago.service';
@Component({
  selector: 'app-pago-realizado',
  templateUrl: './pago-realizado.component.html',
  styleUrls: ['./pago-realizado.component.css']
})
export class PagoRealizadoComponent implements OnInit {

  listPagosRealizados : any[] = []; 
  calculos: any; 
  listaPagoYCalculos: any[] = []; 
  calculo :any [] = [] ; 
  carga = false; 
  idPeriodo:any;
  //alert
  tipoAlerta = '';
  mostrarAlerta =false;
  mensaje='';
  constructor(private router: Router, private pagoService: PagoService,private activatedRoute : ActivatedRoute,) { }
  
  ngOnInit(): void {
    this.activatedRoute.params.subscribe( data => { //recibo el idEmpleado por la ruta y lo almaceno en data
      this.idPeriodo = data['id'];
      this.listarPagosRealizadosPromise()
      .then(data=>{
        if(data===true){
          this.carga=true;
        }
      })
      .catch(error=>{
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
    });
  }
  listarPagosRealizadosPromise = () => {
    const listarPagosRealizadosPromise = new Promise((resolve, reject) => {
      this.pagoService.listarPagosRealizados(this.idPeriodo).subscribe(
        data =>{    
          this.listPagosRealizados = data ;   
          this.listPagosRealizados.sort();
          this.listPagosRealizados.forEach(element => {
            this.obtenerCalculos(element);
          });
          resolve(true);
        },error =>{
          reject(error);
        }
      );
    });
    return listarPagosRealizadosPromise;
  }
  obtenerCalculos(pag:any){  
    this.pagoService.obtenerCaluclos(pag).subscribe(
      data => {
        console.log(data);
        this.calculos = {
          idPago : pag.idPago,
          nombres: pag.contrato.empleado.nombres,
          apellidoPaterno: pag.contrato.empleado.apellidoPaterno,
          apellidoMaterno: pag.contrato.empleado.apellidoMaterno,
          dni: pag.contrato.empleado.dni,
          totalHoras: pag.totalDeHoras,
          valorPorHora:pag.valorPorHora,
          sueldoBasico : data[0],
          totalIngresos : data[1],
          totalDesc : data[2],
          sueldoNeto : data[3]
        }
        this.calculo.push(this.calculos);
        this.calculos = null ;
      },error => {
        console.log("f");
      }

    );
  }
}
