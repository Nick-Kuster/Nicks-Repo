import React from 'react'
import ReactDOM from 'react-dom'
import './index.css'
import { ThemeProvider, createMuiTheme } from '@material-ui/core/styles';
import Videos from './components/Videos';
import Home from './components/Home';
import Nav from './components/Nav';
import Banner  from './components/Banner';
import BackToTop from './components/BackToTop';
import { BrowserRouter as Router, Route, Switch} from 'react-router-dom';

const theme = createMuiTheme({
    palette: {
        primary: {
          light: '#a98274',
          main: '#795548',
          dark: '#4b2c20',
          contrastText: '#fff',
        },
        secondary: {
          light: '#787469',
          main: '#4c493e',
          dark: '#242218',
          contrastText: '#fff',
        },
    },
    typography: {
      h2: {
        fontWeight: 500,        
      },
      h1: {
        fontWeight: 500
      }
    }
});


// Component
//  -State
//  -Lifecycle
//  -UI
class App extends React.Component{
  render(){  
      return ( //JSX
        <Router>
           <ThemeProvider theme={theme}>    
              <Nav/>
              <Switch>
                <Route exact path='/' component={Home}/>
                <Route exact path='/videos' component={Videos}/>
                <Route render={() => <h1>404</h1>}/>
              </Switch>
              <Banner></Banner>            
              <BackToTop/>    
           </ThemeProvider>
        </Router>       
      ) 
  }
}

ReactDOM.render(
    //React Element,
    <App/>,
    // Where to render the Element to
    document.getElementById("app")
)