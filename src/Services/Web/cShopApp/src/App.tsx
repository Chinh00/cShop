import './App.css'
import {Button} from "antd";

function App() {

  return (
    <>
        <Button onClick={() => {
            window.location.href = "https://localhost:5001/connect/authorize?" +
                "client_id=client_react&" +
                "redirect_uri=http://localhost:5173/signin-oidc&" +
                "response_type=code&" +
                "scope=openid%20profile%20api";
        }}>Login</Button>
    </>
  )
}

export default App
