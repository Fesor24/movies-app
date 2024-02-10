export interface IMovieDetail {
  title: string;
  year: string;
  rated: string;
  released: string;
  runtime: string;
  genre: string;
  director: string;
  writer: string;
  actors: string;
  plot: string;
  language: string;
  country: string;
  awards: string;
  poster: string;
  ratings: IRatings[];
  metascore: string;
  imdbRating: string;
  imdbVotes: string;
  imdbId: string;
  dvd: string;
  boxOffice: string;
  production: string;
  website: string;
  type: string;
}

interface IRatings {
  source: string;
  value: string;
}
