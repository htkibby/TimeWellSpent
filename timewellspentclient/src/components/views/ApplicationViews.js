import { Outlet, Route, Routes } from "react-router-dom"
import { AllActivities } from "../activities/AllActivities"

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
         <Route path="activities" element={<AllActivities />} />
      </Routes>
   )
}