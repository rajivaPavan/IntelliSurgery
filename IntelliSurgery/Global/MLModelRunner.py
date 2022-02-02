import pickle
import sys
import json


file_path = "C:\\Users\\pavan\\Documents\\After Alevel\\Projects\\IntelliSurgery\\IntelliSurgery\\MLModels\\model.pkl"
loaded_model = []
with open(file_path, 'rb') as f:
    loaded_model = pickle.load(f)


#Using the data passed to the file
inputData = str(sys.argv[1])
print(inputData)
dataDictionary = json.loads(inputData)

#dataDictionary = {"Age":37,"Gender":1,"ASA":0,"BMI":0.0,"Diseases":["Cancer","Cardiovascular"],"Complication":0,"SurgeryType":"Cardiovascular Surgery"}

age = dataDictionary["Age"]
gender = dataDictionary["Gender"]
asa = dataDictionary["ASA"]
bmi = dataDictionary["BMI"]
diseasesList = dataDictionary["Diseases"]


#Since the diseases related to the patient are passed, initial values are set to zero.
#If the patient has a disease then the value will be changed to 1.
cancer = 0
cardiovascular = 0
dementia = 0
diabetes = 0
digestive = 0
osteoarthritis = 0
pyschologicalDisorder = 0
pulmonary = 0


if (len(diseasesList)== 0):
    pass
else:
    for item in diseasesList:
        if item == "Cancer":
            cancer = 1
        elif item == "Cardiovascular":
            cardiovascular = 1
        elif item == "Dementia":
            dementia =1
        elif item == "Diabetes":
            diabetes = 1
        elif item == "Digestive":
            digestive = 1
        elif item == "Osteoarthritis":
            osteoarthritis = 1
        elif item == "PyschologicalDisorder":
            pyschologicalDisorder = 1
        elif item == "Pulmonary":
            pulmonary = 1
        else:
            pass
        
machineLearningModelInput = [[age,asa,gender,bmi,cancer,cardiovascular,dementia,diabetes,digestive,osteoarthritis,pyschologicalDisorder,pulmonary]]

def predictTheTime(modelInput):
    "This function is used to predict the time for the given surgery"
    predictedOutput = loaded_model.predict(modelInput)
    #print(modelInput, predictedOutput)
    return predictedOutput[0][0];

print(predictTheTime(machineLearningModelInput))