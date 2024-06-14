import React from 'react'
import './App.css'
import LoginPage from './Pages/Login'
import SignUpPage from './Pages/SignUp'
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom'

function App() {
  return (
    <Routes>
      <Route path='/' element={<LoginPage />} />
      <Route path='/signup' element={<SignUpPage />} />
    </Routes>
  )
}

export default App
