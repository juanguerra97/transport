import {NgModule} from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {CoreModule} from "../../core/core.module";
import {HomeBodegasComponent} from './home-bodegas/home-bodegas.component';
import {NewBodegaComponent} from './new-bodega/new-bodega.component';
import {EditBodegaComponent} from './edit-bodega/edit-bodega.component';

const routes: Routes = [
  {path: '', pathMatch: 'full', component: HomeBodegasComponent},
  {path: 'nuevo', component: NewBodegaComponent},
  {path: 'editar/:id', component: EditBodegaComponent},
];

@NgModule({
  declarations: [
    HomeBodegasComponent,
    NewBodegaComponent,
    EditBodegaComponent
  ],
  imports: [
    CoreModule,
    RouterModule.forChild(routes),
  ]
})
export class BodegasModule {
}
