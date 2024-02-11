import { Component, OnInit } from '@angular/core';
import { ISearchHistory, MoviesService } from './movies.service';
import { IMovieSearchResult } from '../shared/models/movie.interface';
import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.scss'],
})
export class MoviesComponent implements OnInit {
  movieSearch!: IMovieSearchResult;

  searchTerm!: string;

  searchTermDisplay!: string;

  itemStart: number = 1;

  itemEnd: number = 10;

  page: number = 1;

  searchHistory$!: Observable<ISearchHistory[]>;

  constructor(
    private movieService: MoviesService,
    private toastrService: ToastrService
  ) {}

  ngOnInit(): void {
    this.searchHistory$ = this.movieService.searchHistory$;
  }

  getRange(num: number): number[] {
    const arrNum = Math.ceil(num / 10);
    return Array.from({ length: arrNum }, (_, index) => index);
  }

  getPageNumber(page: number) {
    this.page = page;
    this.searchMovie(false);
    this.itemStart = (page - 1) * 10 + 1;
    this.itemEnd =
      page * 10 > this.movieSearch.totalRecords
        ? this.movieSearch.totalRecords
        : page * 10;
  }

  setSearchTerm(search: string) {
    this.searchTerm = search;
  }

  searchMovie(resetPage: boolean) {
    if (!this.searchTerm) {
      this.toastrService.error('Enter a valid search term', 'Bad Request!!');
      return;
    }

    if (this.searchTerm.length < 1) return;

    this.addSearchTermToHistory(this.searchTerm);

    this.searchTermDisplay = this.searchTerm;

    if (resetPage) {
      this.page = 1;
      this.itemStart = 1;
      this.itemEnd = 10;
    }

    this.movieService.searchMovie(this.searchTerm, this.page).subscribe({
      next: (data) => (this.movieSearch = data),
      error: (err) => console.log(err),
    });
  }

  addSearchTermToHistory(search: string) {
    this.movieService.addSearchHistory(search);

    this.searchHistory$ = this.movieService.searchHistory$;
  }

  resetSearch() {
    this.searchTerm = '';
    this.searchTermDisplay = '';
    this.movieSearch = {
      totalRecords: 0,
      movies: [],
    };
    this.itemStart = 1;
    this.itemEnd = 10;
  }
}
