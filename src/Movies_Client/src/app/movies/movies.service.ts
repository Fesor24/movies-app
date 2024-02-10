import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { IMovieSearchResult } from '../shared/models/movie.interface';
import { environment } from 'src/environments/environment.development';
import { IMovieDetail } from '../shared/models/movie-detail.interface';
import { BehaviorSubject } from 'rxjs';

export interface ISearchHistory {
  searchTerm: string;
  id: number;
}

@Injectable({
  providedIn: 'root',
})
export class MoviesService {
  baseUrl = environment.apiUrl;

  searchHistorySource = new BehaviorSubject<ISearchHistory[]>([]);

  searchHistory$ = this.searchHistorySource.asObservable();

  constructor(private http: HttpClient) {}

  searchMovie(searchTerm: string, page: number) {
    let httpParams = new HttpParams();

    httpParams = httpParams.set('searchTerm', searchTerm);
    httpParams = httpParams.set('page', page);

    return this.http.get<IMovieSearchResult>(this.baseUrl + 'movies/search', {
      params: httpParams,
    });
  }

  getMovieByImdbId(imdbId: string) {
    let httpParams = new HttpParams();

    httpParams = httpParams.set('imdbId', imdbId);

    return this.http.get<IMovieDetail>(this.baseUrl + 'movies', {
      params: httpParams,
    });
  }

  addSearchHistory(searchTerm: string) {
    let currentSearchHistory = this.getSearchHistoryValue();

    if (
      currentSearchHistory.length < 5 &&
      !currentSearchHistory.some((x) => x.searchTerm === searchTerm)
    ) {
      currentSearchHistory.push({
        searchTerm: searchTerm,
        id: currentSearchHistory.length + 1,
      });
      this.searchHistorySource.next(currentSearchHistory);
    }

    if (currentSearchHistory.length === 5 && !currentSearchHistory
      .some((x) => x.searchTerm === searchTerm)) {
      currentSearchHistory.sort((a, b) => a.id - b.id);
      const oldestItem = currentSearchHistory[0];
      let filteredItem = currentSearchHistory.filter(x => x.id !== oldestItem.id);
      filteredItem.push({
        searchTerm: searchTerm,
        id: currentSearchHistory[4].id + 1
      })
      
      this.searchHistorySource.next(filteredItem);
    }
  }

  getSearchHistoryValue() {
    return this.searchHistorySource.value;
  }
}
