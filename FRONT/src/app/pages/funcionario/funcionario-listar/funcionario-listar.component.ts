import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Funcionario } from '../../../models/funcionario.model';

@Component({
  selector: 'app-funcionario-listar',
  templateUrl: './funcionario-listar.component.html',
  styleUrl: './funcionario-listar.component.css'
})

export class FuncionarioListarComponent {

  funcionarios : Funcionario[] = []

  constructor(
    private client: HttpClient,){}

  ngOnInit() : void{

  this.client.get<Funcionario[]>("https://localhost:7202/api/funcionario/listar")
  .subscribe({
  //Requisição com sucesso
    next: (funcionarios) => {
      this.funcionarios = funcionarios;
      console.table(funcionarios);
    },
    //Requisição com erro
    error: (erro) => {
      console.log(erro);
    }
    })
    }
  
}

