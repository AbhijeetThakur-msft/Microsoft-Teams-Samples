import './App.css';
import {
  BrowserRouter as Router,
  Route,
  Switch
} from 'react-router-dom';
import Configuration from './components/configuration';
import RecruitingDetails from './components/recruiting-details/recruiting-details';
import AddQuestions from './components/recruiting-details/questions/add-questions';

function App() {
  return (
    <Router>
      <Switch>
        <Route exact path='/configure' component={Configuration}></Route>
        <Route exact path='/details' component={RecruitingDetails}></Route>
        <Route exact path='/questions' component={AddQuestions}></Route>
      </Switch>
    </Router>
  );
}

export default App;