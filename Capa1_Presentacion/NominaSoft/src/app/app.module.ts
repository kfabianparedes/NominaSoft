import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule, DatePipe } from '@angular/common';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import {HomeModule} from './home/home.module';
import {PagoModule} from './pago/pago.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RecursosModule } from './recursos/recursos.module';
import {ContratoModule} from './contrato/contrato.module';
@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    RecursosModule,
    HomeModule,
    PagoModule,
    NgbModule,
    ContratoModule,
    
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
