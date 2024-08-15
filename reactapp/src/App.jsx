import { useState, useRef } from 'react';
import { v4 as uuidv4 } from 'uuid'
const guid = uuidv4();

const CryptoCoinsAction = () => {
    const [overall, setOveral] = useState();
    const [initialData, setinitialData] = useState();
    const [file, setFile] = useState();
    const [lastRefreshedAt, setLastRefreshedAt] = useState();
    const interval = useRef();
    const [isButtonDisabled, setButtonDisabled] = useState(true);
    const defaultRefreshRateInSec = 5 * 60;

    const handleButtonClick = () => {
        let refreshRate = document.getElementById('refreshRate').value;
        if (refreshRate != '') {
            let durationVal = parseInt(refreshRate) * 1000;

            if (interval.current != undefined) {
                clearInterval(interval.current);
            }

            interval.current = setInterval(() => {
                refresh();
            }, durationVal);
        }
    };

    const setDefaultValueButtonClick = () => {
        document.getElementById('refreshRate').value = defaultRefreshRateInSec;
        handleButtonClick();
    };

    const requestData = (e) => {
        setFile(e);
        const reader = new FileReader()
        reader.onload = async (e) => {
            const text = (e.target.result)
            var initialBuyDataFromRequestArr = [];
            var toSplitByNewLine = text.split("\n");
            for (var i = 0; i < toSplitByNewLine.length; i++) {
                var toSplitByPipeSymbol = toSplitByNewLine[i].replace(/[\r\n]+$/, '').split("|");
                initialBuyDataFromRequestArr.push({
                    "numberOfCoins": parseFloat(toSplitByPipeSymbol[0]),
                    "cryptoCurrencySymbol": toSplitByPipeSymbol[1],
                    "initialBuyPrice": parseFloat(toSplitByPipeSymbol[2]
                )});
            }
            
            const response = await fetch('https://localhost:5173/coinsportfolio', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ guid, initialBuyDataFromRequestList: initialBuyDataFromRequestArr })
            });

            if (!response.ok) {
                setButtonDisabled(true);
                alert('Error requesting coins data.')
                return;
            }
            const data = await response.json();
            setOveral(data);
            setinitialData(initialBuyDataFromRequestArr);
        };
        reader.readAsText(e.target.files[0])
        setLastRefreshedAt(new Date().toLocaleTimeString());
        setButtonDisabled(false);
    }

    const refresh = () => {
        saveLog('Auto Refresh data')
        requestData(file);
    }

    const refreshButtonClick = () => {
        saveLog('Manual Refresh data')
        requestData(file);
    }    

    const uploadFile = (e) => {
        saveLog('File upload')
        requestData(e);
    }

    const saveLog = (logMessage) => {
        let json = JSON.stringify({ guid: guid, logMessage: logMessage })
        fetch('https://localhost:5173/log/savetolog', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: json
        });
    }

    return (
        <div>
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead style={{ backgroundColor: '#f0f0f0' }}>
                    <tr>
                        <th>Currency</th>
                        <th>Number of coins own</th>
                        <th>Initial buy price in USD</th>
                        <th>Current price in USD</th>
                        <th>Overall in USD</th>
                        <th>Change</th>
                    </tr>
                </thead>
                <tbody>
                    {overall?.calculatedChangeFromInitialBuyList?.map(calculatedChangeFromInitialBuy =>
                        <tr key={calculatedChangeFromInitialBuy?.cryptoCurrency}>
                            <td><b>{calculatedChangeFromInitialBuy?.cryptoCurrency}</b></td>
                            <td>{initialData?.map((q => {
                                if (q.cryptoCurrencySymbol == calculatedChangeFromInitialBuy?.cryptoCurrency) {
                                    return q.numberOfCoins
                                }
                            }))}</td>
                            <td>{initialData?.map((q => {
                                if (q.cryptoCurrencySymbol == calculatedChangeFromInitialBuy?.cryptoCurrency) {
                                    return q.initialBuyPrice
                                }
                            }))}</td>
                            <td>{calculatedChangeFromInitialBuy?.currentPriceInUsd}</td>
                            <td>{calculatedChangeFromInitialBuy?.overallPerCurrencyInUsd}</td>
                            <td>{calculatedChangeFromInitialBuy?.changeInPercentage}%</td>
                        </tr>
                    )}
                </tbody>
            </table>
            <div>
                <span style={{ fontWeight: 'bold' }}>Overall: </span>
                <span>{overall?.overallChangeInPercentage}%</span>
            </div>
            <div>
                <span style={{ fontWeight: 'bold' }}>Overall in USD: </span>
                <span>{overall?.overallChangeInPriceUsd}</span>
            </div>
            <br />
            <div>
                <label style={{ paddingBottom: '10px' }}>
                    Delay Duration (seconds):
                    <input id="refreshRate" type="text" />
                </label>
                <div className="row">
                    <div className="col-sm-2">
                        <button id="refresh" onClick={refreshButtonClick} disabled={isButtonDisabled}>Refresh</button>
                    </div>
                    <div className="col-sm-4">
                        <button onClick={setDefaultValueButtonClick} disabled={isButtonDisabled}>Set Refresh interval to default</button>
                    </div>
                    <div className="col-sm-4">
                        <button onClick={handleButtonClick} disabled={isButtonDisabled}>Set Refresh interval</button>
                    </div>
                </div>
            </div>
            <br/>
            <div>
                <input id="chooseFile_id" type="file" onChange={uploadFile} accept='.txt' />
            </div>
            <br />
            <div>
                Last updated at: {lastRefreshedAt}
            </div>
            <div>
                Guid: {guid}
            </div>
        </div>
    );
};

function App() {
    return (
        <div className="App">
            <CryptoCoinsAction />
        </div>
    );
}

export default

    App;