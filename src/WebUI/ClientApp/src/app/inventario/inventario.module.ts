import {NgModule} from '@angular/core';
import {CoreModule} from "../core/core.module";
import {RouterModule, Routes} from "@angular/router";
import {HomeInventarioComponent} from './home-inventario/home-inventario.component';
import {HomeInventarioBodegaComponent} from './home-inventario-bodega/home-inventario-bodega.component';
import { NewIngresoMaterialComponent } from './new-ingreso-material/new-ingreso-material.component';

const routes: Routes = [
  {path: '', pathMatch: 'full', component: HomeInventarioComponent},
  {path: 'bodega/:id', component: HomeInventarioBodegaComponent},
];

@NgModule({
  declarations: [
    HomeInventarioComponent,
    HomeInventarioBodegaComponent,
    NewIngresoMaterialComponent,
  ],
  imports: [
    CoreModule,
    RouterModule.forChild(routes)
  ]
})
export class InventarioModule {
}
