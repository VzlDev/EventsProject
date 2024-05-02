import './App.css';
import LoginPage from './Pages/Login';
import SignUpPage from './Pages/SignUp';
import { BrowserRouter as Router ,Route, Routes } from 'react-router-dom';
//const express = require('express');
//const cors = require('cors');

//const app = express();

// Enable CORS for all routes
//app.use(cors());

//app.listen(7174, () => {
  //console.log('Server is running on port 7174');
//});

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route path="/signup" element={<SignUpPage />} />
      </Routes>
    </Router>
  );
}

export default App;
