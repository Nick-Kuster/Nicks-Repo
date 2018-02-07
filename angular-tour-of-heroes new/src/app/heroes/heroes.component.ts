import { Component, OnInit } from '@angular/core';
import { Hero } from '../hero';
import { HEROES } from '../mock-heroes';
import { HeroService } from '../hero.service';

@Component({
  selector: 'app-heroes',
  templateUrl: './heroes.component.html',
  styleUrls: ['./heroes.component.css']
})

export class HeroesComponent implements OnInit {

  heroes : Hero[];
  // heroes = HEROES; -- was originally
  // set to the HEROES repo from mock-heroes
  selectedHero: Hero;
  // ={
  //   id: 1,
  //   name: 'Windstorm'
  // }
  // You can initialize objects here if you want.
  // This was removed in tutorial.

  // When Angular creates a HeroesComponent, the Dependency Injection system
  // sets the heroService parameter to the singleton instance of HeroService.
  constructor(private heroService: HeroService) { }

  // https://angular.io/guide/lifecycle-hooks
  ngOnInit() {
    this.getHeroes();
  }

  // getHeroes(): void {
  //   this.heroes = this.heroService.getHeroes();
  // }

  getHeroes() : void {
    this.heroService.getHeroes()
    // subscribe takes the return value of heroservice.getheroes
    // and assigns it to this.heroes
    .subscribe(heroes => this.heroes = heroes);
  }

  onSelect(hero: Hero): void {
    this.selectedHero = hero;
  }

  add(name: string): void{
    name = name.trim();
    if(!name){return; }
    this.heroService.addHero({ name } as Hero)
      // subscribe takes the return value of heroservice.addHero
      // and assigns it to this.heroes
        .subscribe(
          hero => {this.heroes.push(hero);
        });
  }

  // If you neglect to subscribe(), the service will 
  // not send the delete request to the server! As a
  // rule, an Observable does nothing until something subscribes!
  delete(hero: Hero): void {
    this.heroes = this.heroes.filter(h => h!== hero);
    this.heroService.deleteHero(hero).subscribe();
  }
}
