import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PagoRoutingModule } from './pago-routing.module';
import { PagosRegistradosComponent } from './pagos-registrados/pagos-registrados.component';
import {RecursosModule} from '../recursos/recursos.module';
import { PagoRealizadoComponent } from './pago-realizado/pago-realizado.component';
import {NgbAlertModule} from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [
    PagosRegistradosComponent,
    PagoRealizadoComponent
  ],
  imports: [
    CommonModule,
    PagoRoutingModule,
    RecursosModule,
    NgbAlertModule
  ]
})
export class PagoModule { }
