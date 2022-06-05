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
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {
}
