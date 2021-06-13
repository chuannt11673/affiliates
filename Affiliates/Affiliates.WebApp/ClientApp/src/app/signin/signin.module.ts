import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SigninComponent } from './signin.component';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../_shared/material.module';

@NgModule({
    declarations: [
        SigninComponent
    ],
    imports: [ 
        CommonModule,
        RouterModule.forChild([
            {
                path: '',
                component: SigninComponent
            }
        ]),
        ReactiveFormsModule,
        MaterialModule,
    ],
    exports: [],
    providers: [],
})
export class SigninModule {}