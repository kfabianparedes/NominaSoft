import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Contrato} from '../models/contrato.model'

@Injectable({
  providedIn: 'root'
})
export class ContratoService {

  constructor(private http: HttpClient) { }

  obtenerContratoPorIdEmpleado(idEmpleado:number){
    const url = 'http://localhost:44570/api/contrato/'+ idEmpleado ; 
    return this.http.get<any>(url);
  }
  listarContratosPorIdPeriodo(idPeriodo : number){
    const url = 'http://localhost:44570/api/contrato/listarContratos/'+ idPeriodo ; 
    return this.http.get<any>(url);
  }
  editarContrato(contrato:any){
    const url = 'http://localhost:44570/api/contrato'; 
    return this.http.put<number>(url,contrato);
  }
  crearContrato(contrato:any){
    const url = 'http://localhost:44570/api/contrato'; 
    return this.http.post<number>(url,contrato);
  }
  anularContrato(idContrato:number){
    const url = 'http://localhost:44570/api/contrato/anular-contrato/'+idContrato; 
    return this.http.get<number>(url);
  }                                     
  verificarExistenciaContratoVigente(idEmpleado:number){
    const url = 'http://localhost:44570/api/contrato/verificar-vigente/'+idEmpleado; 
    return this.http.get<boolean>(url);
  }
}
