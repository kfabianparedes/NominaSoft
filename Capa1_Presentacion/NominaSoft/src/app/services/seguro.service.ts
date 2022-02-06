import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Seguro } from '../models/seguro.model';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class SeguroService {

  constructor(private http: HttpClient) { }

  obtenerSeguroPorId(idSeguro:number){
    const url = 'http://localhost:44570/api/seguro/id/'+idSeguro ; 
    return this.http.get<Seguro>(url);
  }
  listarSeguros():Observable <Seguro[]>{
    const url = 'http://localhost:44570/api/seguro'; 
    return this.http.get<Seguro[]>(url);
  }
}
  
