import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { ServerErrorHandlerService } from 'src/app/core/utils/services/server-error-handler.service';
import { environment } from 'src/environments/environment.prod';
import { GameMapDto } from '../../dtos/gameMapDto';

@Injectable({
  providedIn: 'root',
})
export class GameMapService {
  constructor(
    private http: HttpClient,
    private errorHandler: ServerErrorHandlerService
  ) {}

  private url = environment.serverUrl + '/GameMap/GetAllGameMaps';

  loadGameMaps(): Observable<GameMapDto[]> {
    return this.http
      .get<GameMapDto[]>(this.url)
      .pipe(catchError(this.errorHandler.handleError));
  }
}
