import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContratoComponent } from './contrato/contrato.component';
import { HomeComponent } from './home/home.component';
import { PagoRealizadoComponent } from './pago/pago-realizado/pago-realizado.component';
import { PagosRegistradosComponent } from './pago/pagos-registrados/pagos-registrados.component';

const routes: Routes = [
  {path: 'home',component: HomeComponent},
  {path: '',component: HomeComponent},
  {path: 'pago/pagos-registrados/:id',component: PagosRegistradosComponent},
  {path: 'contrato/:id',component: ContratoComponent},
  {path: 'pago/pago-realizado/:id', component: PagoRealizadoComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
