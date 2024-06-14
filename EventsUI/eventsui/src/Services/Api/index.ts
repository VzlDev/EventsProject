import axios from 'axios'

const api = axios.create({
  baseURL: 'https://localhost:7174/api',
  headers: {
    'Content-Type': 'application/json',
    Accept: '*/*',
    'Access-Control-Allow-Origin': '*'
  }
})

export default api
