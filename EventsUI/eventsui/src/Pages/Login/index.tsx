import React, { useState, ChangeEvent, FormEvent } from 'react'
import axios from 'axios' // For making HTTP requests to your backend API
import { Button, TextField } from '@mui/material'
import { Link } from 'react-router-dom'
import api from '../../Services/Api'

function LoginPage() {
  const [formData, setFormData] = useState({
    username: '',
    password: ''
  })

  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target
    setFormData({
      ...formData,
      [name]: value
    })
  }

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault()
    try {
      // Make a POST request to your backend API to authenticate the user
      console.log('teste1 ', formData)
      const response = await api.post('/users/login', formData)
      // Handle successful login (e.g., save JWT token, redirect user, etc.)
      console.log('Login successful!', response.data)
    } catch (error) {
      // Handle login error (e.g., display error message to user)
      console.error('Login failed:', error.response.data.error)
    }
  }

  return (
    <div>
      <h2>Login</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Username:</label>
          <TextField
            type='text'
            name='username'
            value={formData.username}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>Password:</label>
          <TextField
            type='password'
            name='password'
            value={formData.password}
            onChange={handleChange}
          />
        </div>
        <Button type='submit'>Login</Button>
        <Link to='/signup'> Sign Up</Link>
      </form>
    </div>
  )
}

export default LoginPage
