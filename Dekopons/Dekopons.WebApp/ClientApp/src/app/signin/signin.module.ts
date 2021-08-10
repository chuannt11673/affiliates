import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SigninComponent } from './signin.component';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../_shared/material/material.module';



@NgModule({
  declarations: [SigninComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        component: SigninComponent
      }
    ]),
    ReactiveFormsModule,
    MaterialModule
  ]
})
export class SigninModule { }
