import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { HomeComponent } from './home/home.component';
import { MoviesModule } from './movies/movies.module';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { MovieItemComponent } from './shared/components/movie-item/movie-item.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { LoadingInterceptor } from './core/interceptors/loading.interceptor';

@NgModule({
  declarations: [AppComponent, HomeComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CoreModule,
    MoviesModule,
    HttpClientModule,
    NgxSpinnerModule,
    BrowserAnimationsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
