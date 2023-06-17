import { Outlet, Route, Routes } from "react-router-dom"
import { AllActivities } from "../activities/AllActivities"
import { MyActivites } from "../activities/MyActivities"

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
         <Route path="myactivities" element={<MyActivites />} />
      </Routes>
   )
}