import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MovieComponent } from './movie/movie.component';
import { MoviesComponent } from './movies.component';
import { MoviesRoutingModule } from './movies-routing.module';
import { SharedModule } from '../shared/shared.module';
import { FormsModule } from '@angular/forms';
import{PaginatorModule} from 'primeng/paginator';



@NgModule({
  declarations: [
    MoviesComponent,
    MovieComponent
  ],
  imports: [
    CommonModule,
    MoviesRoutingModule,
    SharedModule,
    FormsModule,
    PaginatorModule
  ]
})
export class MoviesModule { }
