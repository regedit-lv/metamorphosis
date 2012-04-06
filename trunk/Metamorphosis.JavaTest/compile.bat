jar -cvf generated.jar Generated\*.java metamorphosis\helpers\*.java
javac -cp .;generated.jar mt\MTest.java 
pause