import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { of, switchMap } from 'rxjs';
import { MoviesService } from '../movies.service';
import { IMovieDetail } from 'src/app/shared/models/movie-detail.interface';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.scss']
})
export class MovieComponent implements OnInit {

  movieDetail!: IMovieDetail;

  constructor(private activatedRoute: ActivatedRoute, private movieService: MoviesService){}

  ngOnInit(): void {
    this.getMovie();
  }

  getMovie(){
    this.activatedRoute.paramMap.pipe(
      switchMap((data) => {
        const imdbId = data.get('id');

        if(imdbId){
          return this.movieService.getMovieByImdbId(imdbId)
        }else{
          return of(null)
        }
      })
    ).subscribe({
      next: data => {
        if(data) this.movieDetail = data;
      },
      error: err => console.log(err),
    })
  }

}
