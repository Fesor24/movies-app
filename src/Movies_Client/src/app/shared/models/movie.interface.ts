export interface IMovieSearchResult {
  totalRecords: number;
  movies: IMovie[]
}

export interface IMovie {
  title: string;
  year: string;
  imdbId: string;
  type: string;
  poster: string;
}
