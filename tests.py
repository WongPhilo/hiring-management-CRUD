import unittest
import requests
import threading
import time
import os

def start_dotnet():
    os.system("dotnet run")

class TestEndpoints(unittest.TestCase):
    def test_endpoints(self):
        urls = [
            "Index",
            "Job/Index",
            "Job/Create",
            "Candidate/Index",
            "Candidate/Create",
            "Interview/Index",
            "Interview/Create"
        ]

        for url in urls:
            r = requests.get(url="http://localhost:5000/{}".format(url), verify=False)
            self.assertTrue(r.status_code == 200)

if __name__ == '__main__':
    os.chdir(os.getcwd() + "/App")

    # Build the project
    os.system("dotnet build")

    x = threading.Thread(target=start_dotnet, daemon=True)
    x.start()

    time.sleep(3)
    unittest.main()
    x.join()