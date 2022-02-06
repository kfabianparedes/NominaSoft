import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PagoService {

  constructor(private http: HttpClient) { }

  realizarPagoContratos(contrato:any){
    const url = 'http://localhost:44570/api/pago'; 
    return this.http.post<any>(url,contrato);
  }
  listarPagosRealizados(idPeriodo:number){
    const url = 'http://localhost:44570/api/pago/'+idPeriodo; 
    return this.http.get<any>(url);
  }
obtenerCaluclos(pago:any){
    const url = 'http://localhost:44570/api/pago/calcular'; 
    return this.http.post<number[]>(url,pago);
  }
}
