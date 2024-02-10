import { Component, Input } from '@angular/core';
import { IMovie } from '../../models/movie.interface';
import { Router } from '@angular/router';

@Component({
  selector: 'app-movie-item',
  templateUrl: './movie-item.component.html',
  styleUrls: ['./movie-item.component.scss'],
})
export class MovieItemComponent {
  @Input() movieItem!: IMovie;

  constructor(private router: Router) {}

  navigateToDetailsPage() {
    this.router.navigate([`/movies/${this.movieItem.imdbId}`]);
  }
}
