import { Route, Routes } from "react-router-dom"
import { ApplicationViews } from "./components/views/ApplicationViews"
import { Login } from "./components/auth/Login";
import { Register } from "./components/auth/Register";
import { Authorized } from "./components/views/Authorized";
import { NavBar } from "./components/nav/NavBar";

export const TimeWellSpent = () => {
   return (
     <Routes>
       <Route path="/Login" element={<Login />} />
       <Route path="/register" element={<Register />} />
 
       <Route
         path="*"
         element={
           <Authorized>
             <>
               <NavBar />
               <ApplicationViews />
             </>
           </Authorized>
         }
       />
     </Routes>
   );
 };