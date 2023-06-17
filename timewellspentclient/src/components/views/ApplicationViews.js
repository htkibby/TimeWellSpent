import { Outlet, Route, Routes } from "react-router-dom"
import { AllActivities } from "../activities/AllActivities"
import { MyActivites } from "../activities/MyActivities"
import { CustomActivityForm } from "../activities/CustomActivityForm"

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
         <Route path="activityform" element={<CustomActivityForm />} />
      </Routes>
   )
}