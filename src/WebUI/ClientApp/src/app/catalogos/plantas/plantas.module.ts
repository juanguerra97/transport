import {NgModule} from '@angular/core';
import {CoreModule} from "../../core/core.module";
import {HomePlantasComponent} from "./home-plantas/home-plantas.component";
import {RouterModule, Routes} from "@angular/router";
import {NewPlantaComponent} from './new-planta/new-planta.component';
import {EditPlantaComponent} from './edit-planta/edit-planta.component';

const routes: Routes = [
  {path: '', pathMatch: 'full', component: HomePlantasComponent},
  {path: 'nuevo', component: NewPlantaComponent},
  {path: 'editar/:id', component: EditPlantaComponent},
];

@NgModule({
  declarations: [HomePlantasComponent, NewPlantaComponent, EditPlantaComponent],
  imports: [
    CoreModule,
    RouterModule.forChild(routes)
  ]
})
export class PlantasModule {
}
