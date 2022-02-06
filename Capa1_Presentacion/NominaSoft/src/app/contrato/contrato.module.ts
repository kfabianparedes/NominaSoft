import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
//import {ContratoComponent} from '../contrato/contrato.component'
import { ContratoRoutingModule } from './contrato-routing.module';
import {ContratoComponent} from '../contrato/contrato.component'


//Modulos externos
import {RecursosModule} from '../recursos/recursos.module';
import {NgbAlertModule} from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
@NgModule({
  declarations: [
    ContratoComponent
  ],
  imports: [
    CommonModule,
    ContratoRoutingModule,
    RecursosModule,
    ReactiveFormsModule,
    FormsModule,
    NgbAlertModule,
    
  ],
  providers: [DatePipe]
})
export class ContratoModule { }
