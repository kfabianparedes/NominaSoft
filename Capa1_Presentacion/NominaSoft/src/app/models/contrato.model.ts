export interface Contrato{
    idContrato: number;
    esVigente:boolean;
    fechaInicial:any;
    fechaFinal:any;
    tieneAsignacionFamiliar:boolean;
    totalHorasPorSemana: number;
    valorPorHora:number;
    estaAnulado:boolean;
    cargo:String;
    seguro:String;
    empleado:String;
    
}