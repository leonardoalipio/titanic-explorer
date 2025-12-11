import { use, useState } from "react";


const fetchPassengers = fetch("http://localhost:8080/passengers")
.then((response) => response.json());

const usePassengers = () => {
  
  const passengersResult = use(fetchPassengers);
  const [passengers, setPassengers] = useState(passengersResult)
  
  return {  
    passengers, 
    setPassengers,
  };
}

export default usePassengers;