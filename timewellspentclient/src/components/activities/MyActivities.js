import { useEffect, useState } from "react";
import { FetchActivitiesByUserEmail } from "../ApiManager";
import { Button, Card, CardActions, CardContent, CardMedia, Grid, Typography } from "@mui/material";

export const MyActivites = () => {
   const [myActivities, setActivities] = useState([]);

   const localTimeUser = localStorage.getItem("capstone_user");
   const timeUserObject = JSON.parse(localTimeUser);

   const getActivitiesFromRepo = async () => {
      const gottenActivities = await FetchActivitiesByUserEmail(timeUserObject.email);
      setActivities(gottenActivities);
   };
   
   useEffect(() => {
      getActivitiesFromRepo();
   }, []);

   return (
      <>
      <Grid container spacing={2}>
         {myActivities.map(
            (activity) => {
               return (
                  <Grid item xs={3}>
                     <Card sx={{ maxWidth: 275 }} key={`activity--${activity.id}`} variant="outlined">
                        <CardContent>
                           <Typography sx={{ fontSize: 28 }} color="text.primary" gutterBottom>
                              {activity.name}
                           </Typography>
                           <CardMedia
                              component="img"
                              image={activity.image}
                              height="200" 
                              />
                        </CardContent>
                        <CardActions>
                           <Button size="small">Learn More</Button>
                        </CardActions>
                     </Card>
                  </Grid>
               )
            }
         )}
      </Grid>
      </>
   )
}