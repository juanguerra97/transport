import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {AuthorizeGuard} from '../api-authorization/authorize.guard';
import {HomeComponent} from './home/home.component';
import {TokenComponent} from './token/token.component';

export const routes: Routes = [
  {path: '', component: HomeComponent, pathMatch: 'full'},
  {path: 'token', component: TokenComponent, canActivate: [AuthorizeGuard]},
  {
    path: 'admin', canActivate: [AuthorizeGuard], children: [
      {path: 'catalogo', loadChildren: () => import('./catalogos/catalogos.module').then(m => m.CatalogosModule)}
    ]
  },
  {path: 'inventario', loadChildren: () => import('./inventario/inventario.module').then(m => m.InventarioModule)},
  {path: 'pedidos', loadChildren: () => import('./pedidos/pedidos.module').then(m => m.PedidosModule)},
  {path: '', loadChildren: () => import('./conductores/conductores.module').then(m => m.ConductoresModule)},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {
}
