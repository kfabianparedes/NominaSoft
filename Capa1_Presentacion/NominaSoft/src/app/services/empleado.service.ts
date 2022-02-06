import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Empleado } from '../models/empleado.model';

@Injectable({
  providedIn: 'root'
})
export class EmpleadoService {

  constructor(private http: HttpClient) { }

  obtenerEmpleadoPorDNI(dni:number){
    const url = 'http://localhost:44570/api/empleado/'+ dni ; 
    return this.http.get<any>(url);
  }
  obtenerEmpleadoPorId(id:number){
    const url = 'http://localhost:44570/api/empleado/id/'+ id ; 
    return this.http.get<Empleado>(url);
  }
}
