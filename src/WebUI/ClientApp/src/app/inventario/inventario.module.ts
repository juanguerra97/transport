import {NgModule} from '@angular/core';
import {CoreModule} from "../core/core.module";
import {RouterModule, Routes} from "@angular/router";
import {HomeInventarioComponent} from './home-inventario/home-inventario.component';
import {HomeInventarioBodegaComponent} from './home-inventario-bodega/home-inventario-bodega.component';
import { NewIngresoMaterialComponent } from './new-ingreso-material/new-ingreso-material.component';
import { PedidosBodegaComponent } from './pedidos-bodega/pedidos-bodega.component';
import { NewPedidoMaterialComponent } from './new-pedido-material/new-pedido-material.component';
import { EditPedidoMaterialComponent } from './edit-pedido-material/edit-pedido-material.component';
import { ListMovimientosComponent } from './list-movimientos/list-movimientos.component';

const routes: Routes = [
  {path: '', pathMatch: 'full', component: HomeInventarioComponent},
  {path: 'bodega/:id', component: HomeInventarioBodegaComponent},
];

@NgModule({
  declarations: [
    HomeInventarioComponent,
    HomeInventarioBodegaComponent,
    NewIngresoMaterialComponent,
    PedidosBodegaComponent,
    NewPedidoMaterialComponent,
    EditPedidoMaterialComponent,
    ListMovimientosComponent,
  ],
  imports: [
    CoreModule,
    RouterModule.forChild(routes)
  ]
})
export class InventarioModule {
}
