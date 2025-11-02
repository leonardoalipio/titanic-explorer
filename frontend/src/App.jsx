import { Suspense } from "react"
import usePassengers from "./components/usePassengers";
import Layout from "./components/layout/Layout"
import PassengerList from "./components/PassengerList"
import PassengerSearch from "./components/PassengerSearch";

function App() {
  
  const { passengers, setPassengers } = usePassengers();

  return (
    <>
      <Layout>
        <div className="row g-5">
          <div className="col">
            <h3 className="pb-4 mb-4 fst-italic border-bottom">List of Passengers of RMS Titanic</h3>
              <p>Select any filter to see passengers:</p>
              <PassengerSearch setPassengers={setPassengers} />
              <Suspense fallback={<div>Loading passengers...</div>}>
                <PassengerList passengers={passengers}/>
              </Suspense>
          </div>
        </div>
      </Layout>
    </>
  )
}

export default App
