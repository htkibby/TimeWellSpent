import { Route, Routes } from "react-router-dom"

export const ApplicationViews = () => {

   return (
      <Routes>
         <Route path="/" element={
            <>
               <h1>Time Well Spent</h1>

               <Outlet />
            </>
         }>
         </Route>
         <Route path="activities"></Route>
      </Routes>
   )
}