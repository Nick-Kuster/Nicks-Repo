import { Component, OnInit } from '@angular/core';

import { Observable } from 'rxjs/Observable';
import { Subject }    from 'rxjs/Subject';
import { of }         from 'rxjs/observable/of';

import {
   debounceTime, distinctUntilChanged, switchMap
 } from 'rxjs/operators';

import { Hero } from '../hero';
import { HeroService } from '../hero.service';

@Component({
  selector: 'app-hero-search',
  templateUrl: './hero-search.component.html',
  styleUrls: [ './hero-search.component.css' ]
})

export class HeroSearchComponent implements OnInit {
  heroes$: Observable<Hero[]>;
  private searchTerms = new Subject<string>();

  constructor(private heroService: HeroService) {}

  // Push a search term into the observable stream.
  search(term: string): void{
    this.searchTerms.next(term);
    console.log(this.heroes$);
  }

  // A Subject is both a source of observable values and an 
  // Observable itself. You can subscribe to a Subject as you would any Observable.

  // You can also push values into that Observable by calling 
  // its next(value) method as the search() method does.
  
  // The search() method is called via an event binding to the 
  // textbox's keystroke event.
  ngOnInit(): void {
    console.log("ngOnInit has fired off!");
    this.heroes$ = this.searchTerms.pipe(
      
      // Waits until the flow of new string events 
      // pauses for 300 milliseconds before passing along 
      // the latest string. You'll never make requests more frequently than 300ms.
      debounceTime(300),

      // --Ignore new term if same as previous term.
      // --Ensures that a request is sent only if the filter text changed.
      distinctUntilChanged(),

      // --Switch to new search observable each time the term changes
      // --Calls the search service for each search term that makes it through 
      // debounce and distinctUntilChanged. It cancels and discards previous s
      // earch observables, returning only the latest search service observable.
      switchMap((term: string) => this.heroService.searchHeroes(term)),
    );
  }

}
