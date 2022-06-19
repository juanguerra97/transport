import {NgModule} from '@angular/core';
import {CoreModule} from "../core/core.module";
import {RouterModule, Routes} from "@angular/router";
import {HomePedidosComponent} from './home-pedidos/home-pedidos.component';
import {FiltroPedidosComponent} from './filtro-pedidos/filtro-pedidos.component';
import {AdministracionPedidoComponent} from './administracion-pedido/administracion-pedido.component';

const routes: Routes = [
  {path: '', pathMatch: 'full', component: HomePedidosComponent},
  {
    path: ':pedidoId', children: [
      {path: '', pathMatch: 'full', component: AdministracionPedidoComponent}
    ]
  }
];

@NgModule({
  declarations: [
    HomePedidosComponent,
    FiltroPedidosComponent,
    AdministracionPedidoComponent
  ],
  imports: [
    CoreModule,
    RouterModule.forChild(routes),
  ]
})
export class PedidosModule {
}
