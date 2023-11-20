import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { FuncionarioListarComponent } from "./pages/funcionario/funcionario-listar/funcionario-listar.component";

const routes: Routes = [
  {
    path: "",
    component: FuncionarioListarComponent,
  },
  {
    path:"pages/funcionario/listar",
    component: FuncionarioListarComponent,
  },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}

