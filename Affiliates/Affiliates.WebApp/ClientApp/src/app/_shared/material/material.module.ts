import { NgModule } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';

const modules = [
  MatSidenavModule
];

@NgModule({
  declarations: [],
  imports: [
    ...modules
  ],
  exports: [
    ...modules
  ]
})
export class MaterialModule { }
