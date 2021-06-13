import { NgModule } from '@angular/core';
import { MatButtonModule, MatFormFieldModule, MatInputModule } from '@angular/material';

const modules = [
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
];

@NgModule({
    imports: [
        modules
    ],
    exports: [
        modules
    ],
})
export class MaterialModule {}