import {NgModule} from '@angular/core';
import {CoreModule} from "../core/core.module";
import {RouterModule, Routes} from "@angular/router";
import {HomeInventarioComponent} from './home-inventario/home-inventario.component';

const routes: Routes = [
  {path: '', pathMatch: 'full', component: HomeInventarioComponent}
];

@NgModule({
  declarations: [
    HomeInventarioComponent
  ],
  imports: [
    CoreModule,
    RouterModule.forChild(routes)
  ]
})
export class InventarioModule {
}
