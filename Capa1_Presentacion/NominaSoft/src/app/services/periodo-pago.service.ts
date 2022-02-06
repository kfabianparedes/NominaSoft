import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PeriodoDePago } from '../models/periodo.model';

@Injectable({
  providedIn: 'root'
})
export class PeriodoPagoService {

  constructor(private http: HttpClient) { }

  obtenerPeriodoPagoActivo(){
      const url = 'http://localhost:44570/api/periodo'; 
      return this.http.get<PeriodoDePago>(url);
  }
  verificarProcesarPeriodo(periodo: PeriodoDePago){
    const url = 'http://localhost:44570/api/periodo'; 
    return this.http.post<any>(url,periodo);
  }
  cambiarProcesado(idPeriodo:number){
    const url = 'http://localhost:44570/api/periodo'; 
    return this.http.put<boolean>(url,idPeriodo);
  }
  
}
