export class Evento{
    Titulo: string = "";
    Descripcion: string = "";
    Fecha: string = "";
    Tipo: string = "";
    Estado: string =  "";

    constructor(t_: string, d_: string, f_: string, ty_: string, e_: string){
        this.Titulo = t_;
        this.Descripcion = d_;
        this.Fecha = f_;
        this.Tipo = ty_;
        this.Estado = e_;
    }

    get getTitle(): string{
        return this.Titulo;
    }

    get getDescription(): string{
        return this.Descripcion;
    }

    get getTipo(): string{
        return this.Fecha;
    }

    get getEstado(): string{
        return this.Estado;
    }

    public editTitle(title: string){
        this.Titulo = title;
    }

    public editDescription(description: string){
        this.Descripcion = description;
    }

    public editDate(date: string){
        this.Fecha = date;
    }

    public editType(type: string){
        this.Tipo = type;
    }

    public editEstado(state: string){
        this.Estado = state;
    }
}