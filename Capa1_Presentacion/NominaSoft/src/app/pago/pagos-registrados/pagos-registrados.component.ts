import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import {ContratoService} from '../../services/contrato.service'; 
import {ActivatedRoute, Router} from '@angular/router';
import {PagoService} from '../../services/pago.service';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
@Component({
  selector: 'app-pagos-registrados',
  templateUrl: './pagos-registrados.component.html',
  styleUrls: ['./pagos-registrados.component.css']
})
export class PagosRegistradosComponent implements OnInit {
  
  constructor(private contratoService: ContratoService,
              private activatedRoute : ActivatedRoute,
              private pagoService : PagoService,
              private modal : NgbModal,
              private router: Router) { }

  contratosListados :any[] = []; 

  carga = false;
  idPeriodo : any ; 
  @ViewChild('confirmaciónPago') modalConfirmaPag!: ElementRef;
  ngOnInit(): void {
    this.activatedRoute.params.subscribe( data => { //recibo el idEmpleado por la ruta y lo almaceno en data
      this.idPeriodo =data['id']; 
      this.listarContratos(data['id']);
    });
  }

  listarContratos(idPeriodo: number){
    this.contratoService.listarContratosPorIdPeriodo(idPeriodo).subscribe(data => {
    this.contratosListados = data; 
    this.carga = true;    
    },
    error => {
      console.log("F");
    });

  }
  
  realizarPago(){
  this.pagoService.realizarPagoContratos(this.contratosListados).subscribe(
    data=>{
      console.log(data);
      setTimeout(() =>{
        this.router.navigate(['pago/pago-realizado',this.idPeriodo]),
        this.modal.dismissAll();
      },1000);
    },error =>{

    });
  }

  abrirModalConfirmacionPago() {
    this.modal.open(this.modalConfirmaPag);
  }
}
